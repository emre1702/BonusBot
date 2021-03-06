export interface NavigationButton {
    icon: string;
    name: string;
    url: string;
    module?: string;
    alwaysAccess?: boolean;
    hidden?: boolean;
    confirmationTextKey?: string;
}
