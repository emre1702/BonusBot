import { NavigationButton } from './modules/page/models/navigation-button';

export class Constants {
    static navigationButtons: NavigationButton[] = [
        { icon: 'home', name: 'Home', url: '/dashboard' },
        { icon: 'queue_music', name: 'Audio', url: '/audio', module: 'Audio' },
        { icon: '', name: '', url: '/login', alwaysAccess: true },
    ];
}
