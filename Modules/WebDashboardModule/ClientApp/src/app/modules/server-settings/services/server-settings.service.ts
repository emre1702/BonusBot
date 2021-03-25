import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import api from 'src/app/routes/api';
import { CommandService } from '../../shared/services/command.service';
import { ModuleData } from '../models/module-data';
import { ModuleSetting } from '../models/module-setting';

@Injectable()
export class ServerSettingsService {
    constructor(private readonly httpClient: HttpClient, private readonly commandService: CommandService) {}

    loadAllModuleDatas(guildId: string): Observable<ModuleData[]> {
        return this.httpClient.get<ModuleData[]>(api.get.settings.allModuleDatas, { params: { guildId } });
    }

    loadModuleSettings(guildId: string, moduleName: string): Observable<ModuleSetting[]> {
        return this.httpClient.get<ModuleSetting[]>(api.get.settings.moduleSettings, { params: { guildId, moduleName } });
    }

    setModuleActive(moduleData: ModuleData) {
        const addOrRemove = moduleData.isActive ? 'add' : 'remove';
        return this.commandService.execute(`module ${addOrRemove} ${moduleData.name}`);
    }

    setModuleSetting(moduleName: string, moduleSetting: ModuleSetting) {
        return this.commandService.execute(`config ${moduleName} ${moduleSetting.name} ${moduleSetting.value}`);
    }
}
