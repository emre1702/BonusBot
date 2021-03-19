FROM mcr.microsoft.com/dotnet/sdk:latest AS build-env
ARG CERTIFICATE_PASSWORD
ENV ISDOCKER="true"
WORKDIR /bonusbot-source

COPY . .

RUN apt-get update && apt-get install -y \
    nodejs \
    npm \
	&& rm -rf /var/lib/apt/lists/* 

RUN dotnet publish ./Core/Core.csproj -p:PublishProfile=Linux
RUN cp ./Modules/WebDashboardBoardModule/ClientApp/dist/* ./Build/wwwroot

FROM mcr.microsoft.com/dotnet/aspnet:latest AS release
ENV ISDOCKER="true"

RUN apt-get update && apt-get install -y \
    libc6-dev \
	libunwind8 \
    libssl1.1 \
    locales \
    tzdata \
	&& rm -rf /var/lib/apt/lists/* 

RUN useradd -m -d /home/bonusbot bonusbot

WORKDIR /home/bonusbot

COPY --from=build-env /bonusbot-source/Build .

RUN [ ! -d /bonusbot-data ] && mkdir -p /bonusbot-data; exit 0

ADD ./entrypoint.sh ./entrypoint.sh
EXPOSE 443/tcp 3000 5000

RUN chown -R bonusbot:bonusbot . \
    && chown -R bonusbot:bonusbot /bonusbot-data \
    && chmod +x BonusBot.Core \
    && chmod +x entrypoint.sh

RUN rm -rf /bonusbot-source

USER bonusbot
CMD ./entrypoint.sh
ENTRYPOINT ["/bin/sh", "./entrypoint.sh"]