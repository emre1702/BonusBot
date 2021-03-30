import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import api from 'src/app/routes/api';
import { CommandService } from '../../shared/services/command.service';
import { ServerSettingType } from '../enums/server-setting-type';
import { ModuleData } from '../models/module-data';
import { ServerSettingDataByKey } from '../models/server-setting-data-by-key';

@Injectable()
export class ServerSettingsService {
    constructor(private readonly httpClient: HttpClient, private readonly commandService: CommandService) {}

    loadAllModuleDatas(guildId: string): Observable<ModuleData[]> {
        return this.httpClient.get<ModuleData[]>(api.get.settings.allModuleDatas, { params: { guildId } });
    }

    loadModuleSettings(guildId: string, moduleName: string): Observable<ServerSettingDataByKey> {
        return this.httpClient.get<ServerSettingDataByKey>(api.get.settings.moduleSettings, { params: { guildId, moduleName } });
    }

    setModuleActive(moduleData: ModuleData) {
        const addOrRemove = moduleData.isActive ? 'add' : 'remove';
        return this.commandService.execute(`module ${addOrRemove} ${moduleData.name}`);
    }

    setModuleSetting(moduleName: string, settingKey: string, value: unknown, type: ServerSettingType) {
        const configSuffix = this.getConfigSuffix(type);
        return this.commandService.execute(`Config${configSuffix} ${moduleName} ${settingKey} ${String(value)}`);
    }

    private getConfigSuffix(type: ServerSettingType): string {
        switch (type) {
            case ServerSettingType.integerSlider:
                return ServerSettingType[ServerSettingType.integer].toTitleCase();
            case ServerSettingType.locale:
            case ServerSettingType.timeZone:
                return ServerSettingType[ServerSettingType.string].toTitleCase();
            default:
                return ServerSettingType[type].toTitleCase();
        }
    }
}
