import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GamePlaningService } from '../../services/game-planing.service';
import { MobileService } from 'src/app/modules/shared/services/mobile.service';
import { NgxMatDateAdapter } from '@angular-material-components/datetime-picker';
import { Constants } from 'src/app/constants';

@Component({
    selector: 'app-game-planing',
    templateUrl: './game-planing.component.html',
    styleUrls: ['../../../../styles/setting-styles.scss'],
    providers: [GamePlaningService],
})
export class GamePlaningComponent {
    formGroup = new FormGroup({
        // eslint-disable-next-line @typescript-eslint/unbound-method
        game: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(300)]),
        // eslint-disable-next-line @typescript-eslint/unbound-method
        date: new FormControl(new Date(), [Validators.required]),
    });

    minDate = new Date();
    maxDate = new Date(this.minDate.getTime() + Constants.maxGamePlaningDaysBefore * 24 * 60 * 60 * 1000);

    constructor(readonly service: GamePlaningService, readonly mobileService: MobileService, private readonly adapter: NgxMatDateAdapter<Date>) {
        adapter.setLocale('de-DE');
    }
}
