import { NavigationButton } from './modules/navigation/models/navigation-button';

export class Constants {
    static buttons: NavigationButton[] = [
        { icon: 'home', name: 'Home', url: '/dashboard' },
        { icon: 'queue_music', name: 'Audio', url: '/audio', module: 'Audio' },
        { icon: '', name: '', url: '/login', alwaysAccess: true },
    ];
}
