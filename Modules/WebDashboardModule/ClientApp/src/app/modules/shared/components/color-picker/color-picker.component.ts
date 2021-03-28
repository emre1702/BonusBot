/* eslint-disable @typescript-eslint/no-empty-function */
import { FocusMonitor } from '@angular/cdk/a11y';
import { Component, DoCheck, ElementRef, forwardRef, HostBinding, Injector, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { Subject } from 'rxjs';
import { MobileService } from '../../services/mobile.service';
import { Color, NgxMatColorPickerComponent } from '@angular-material-components/color-picker';

@Component({
    selector: 'app-color-picker',
    templateUrl: './color-picker.component.html',
    styleUrls: ['./color-picker.component.scss'],
    providers: [
        { provide: MatFormFieldControl, useExisting: ColorPickerComponent },
        { provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => ColorPickerComponent), multi: true },
    ],
})
export class ColorPickerComponent implements MatFormFieldControl<Color>, ControlValueAccessor, OnInit, OnDestroy, DoCheck {
    @ViewChild(NgxMatColorPickerComponent, { static: true }) colorPicker: NgxMatColorPickerComponent;
    @ViewChild(MatInput, { static: true }) input: MatInput;

    @Input() disabled: boolean;

    placeholder: string;
    focused: boolean;
    get empty(): boolean {
        return this.input.empty;
    }

    @HostBinding('class.floating')
    get shouldLabelFloat() {
        return this.focused || !this.empty;
    }

    @Input() required: boolean;

    private _errorState: boolean;
    get errorState(): boolean {
        return this._errorState;
    }
    set errorState(value: boolean) {
        this._errorState = value;
        this.input.errorState = value;
    }
    controlType = 'color-picker';
    autofilled?: boolean;

    @HostBinding() get id(): string {
        return this.input.id;
    }

    private _value: Color;
    get value(): Color {
        return this._value;
    }
    set value(value: Color) {
        const oldRgba = this._value?.rgba;
        this._value = value;
        if (!oldRgba?.length || value?.rgba === oldRgba) return;
        this.onTouched();
        this.onChanged(value);
        this.stateChanges.next();
    }

    get userAriaDescribedBy(): string {
        return this.input.userAriaDescribedBy;
    }

    ngControl: NgControl;
    stateChanges = new Subject<void>();

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    onChanged = (_: Color) => {};
    onTouched = () => {};

    constructor(
        readonly mobileService: MobileService,
        private readonly fm: FocusMonitor,
        private readonly elRef: ElementRef<HTMLElement>,
        private readonly injector: Injector
    ) {
        fm.monitor(elRef, true).subscribe((origin) => {
            this.focused = !!origin;
            this.stateChanges.next();
        });
    }

    ngOnInit() {
        this.ngControl = this.injector.get(NgControl);
        if (this.ngControl != null) this.ngControl.valueAccessor = this;
    }

    ngOnDestroy() {
        this.stateChanges.complete();
        this.fm.stopMonitoring(this.elRef);
    }

    ngDoCheck() {
        if (this.ngControl) {
            this.errorState = this.ngControl.invalid && this.ngControl.touched;
            this.stateChanges.next();
        }
    }

    setDescribedByIds(ids: string[]): void {
        this.input.setDescribedByIds(ids);
    }

    onContainerClick(): void {
        if (!this.colorPicker.opened) this.colorPicker.open();
    }

    writeValue(value: Color): void {
        this.value = value;
    }

    registerOnChange(fn: (value: Color) => void): void {
        this.onChanged = fn;
    }

    registerOnTouched(fn: () => void): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }
}
