import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GamePlaningComponent } from './components/game-planing/game-planing.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    declarations: [GamePlaningComponent],
    imports: [CommonModule, FormsModule, MaterialModule, SharedModule],
})
export class GamePlaningModule {}
