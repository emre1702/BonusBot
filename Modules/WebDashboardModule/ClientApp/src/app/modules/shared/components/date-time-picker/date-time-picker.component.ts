/* eslint-disable @typescript-eslint/no-empty-function */
import { NgxMatDateAdapter, NgxMatDatetimePicker } from '@angular-material-components/datetime-picker';
import { FocusMonitor } from '@angular/cdk/a11y';
import { Component, DoCheck, ElementRef, forwardRef, HostBinding, Injector, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatFormFieldControl } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { Subject } from 'rxjs';
import { MobileService } from '../../services/mobile.service';

@Component({
    selector: 'app-date-time-picker',
    templateUrl: './date-time-picker.component.html',
    providers: [
        { provide: MatFormFieldControl, useExisting: DateTimePickerComponent },
        { provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => DateTimePickerComponent), multi: true },
    ],
})
export class DateTimePickerComponent implements MatFormFieldControl<Date>, ControlValueAccessor, OnInit, OnDestroy, DoCheck {
    @ViewChild(NgxMatDatetimePicker, { static: true }) dateTimerPicker: MatDatepicker<Date>;
    @ViewChild(MatInput, { static: true }) input: MatInput;

    @Input() minDate?: Date = new Date(0);
    @Input() maxDate?: Date = new Date('01.01.2050');

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
    controlType = 'date-time-picker';
    autofilled?: boolean;

    @HostBinding() get id(): string {
        return this.input.id;
    }

    private _value: Date;
    get value(): Date {
        return this._value;
    }
    set value(value: Date) {
        this._value = value;
        this.stateChanges.next();
        this.onTouched();
        this.onChanged(value);
    }

    get userAriaDescribedBy(): string {
        return this.input.userAriaDescribedBy;
    }

    ngControl: NgControl;
    stateChanges = new Subject<void>();

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    onChanged = (_: Date) => {};
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
        if (!this.dateTimerPicker.opened) this.dateTimerPicker.open();
    }

    writeValue(value: Date): void {
        this.value = value;
    }

    registerOnChange(fn: (value: Date) => void): void {
        this.onChanged = fn;
    }

    registerOnTouched(fn: () => void): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }
}
