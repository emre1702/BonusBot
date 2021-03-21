using BonusBot.AudioModule.Commands.Queue;
using BonusBot.AudioModule.Preconditions;
using Discord.Commands;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.PartialMain
{
    partial class Main
    {
        [Command("Queue")]
        [Alias("Warteschlange", "Warteschleife")]
        [RequirePlayer(false)]
        public Task OutputQueue()
           => new OutputQueue(this).Do(new());

        [Command("DeleteQueue")]
        [Alias("DelQueue", "QueueDelete", "QueueDel", "LöscheWarteschlange", "WarteschlangeLöschen")]
        [RequirePlayer(false)]
        public Task DeleteQueue(int queueNumber)
            => new DeleteQueue(this).Do(new(queueNumber));

        [Command("shuffle")]
        [Alias("mix")]
        [RequirePlayer]
        public Task Shuffle()
         => new Shuffle(this).Do(new());
    }
}
