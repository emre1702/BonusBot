import { NavigationButton } from './modules/page/models/navigation-button';

export class Constants {
    static navigationButtons: NavigationButton[] = [
        { icon: 'home', name: 'Home', url: '/dashboard' },
        { icon: 'queue_music', name: 'Audio', url: '/audio', module: 'Audio' },
        { icon: 'videogame_asset', name: 'GamePlaning', url: '/gameplaning', module: 'GamePlaning' },
        { icon: 'settings', name: 'Settings', url: '/settings', module: 'GuildSettings' },

        { icon: '', name: '', url: '/login', alwaysAccess: true, hidden: true },
    ];

    static maxGamePlaningDaysBefore = 7;
}
