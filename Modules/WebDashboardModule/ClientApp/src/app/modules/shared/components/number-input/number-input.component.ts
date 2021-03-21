import { FocusMonitor } from '@angular/cdk/a11y';
import { coerceBooleanProperty, coerceNumberProperty } from '@angular/cdk/coercion';
import { Component, ElementRef, HostBinding, Input, OnDestroy, Optional, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-number-input',
    templateUrl: './number-input.component.html',
    styleUrls: ['./number-input.component.scss'],
    providers: [{ provide: MatFormFieldControl, useExisting: NumberInputComponent }],
})
export class NumberInputComponent implements MatFormFieldControl<number>, ControlValueAccessor, OnDestroy {
    static currentId = 0;

    @ViewChild(MatInput, { static: false }) input: MatInput;

    @Input() min = Number.MIN_SAFE_INTEGER;
    @Input() max = Number.MAX_SAFE_INTEGER;

    private _value: number;
    get value(): number {
        return this._value;
    }
    set value(number: number) {
        number = coerceNumberProperty(number);
        number = Math.min(this.max, Math.max(number, this.min));
        this._value = number;
        this.stateChanges.next();
        this.onTouched();
        this.onChanged(number);
        // Without this the input won't update the value
        this.input.value = String(number);
    }

    @Input()
    get placeholder() {
        return this._placeholder;
    }
    set placeholder(plh) {
        this._placeholder = plh;
        this.stateChanges.next();
    }
    private _placeholder: string;

    focused = false;

    get empty() {
        return !this.value;
    }

    @HostBinding() id = `number-input-${++NumberInputComponent.currentId}`;
    @HostBinding('class.floating')
    get shouldLabelFloat() {
        return this.focused || !this.empty;
    }

    @Input()
    get required() {
        return this._required;
    }
    set required(req) {
        this._required = coerceBooleanProperty(req);
        this.stateChanges.next();
    }
    private _required = false;

    @Input()
    get disabled(): boolean {
        return this._disabled;
    }
    set disabled(value: boolean) {
        this._disabled = coerceBooleanProperty(value);
        this.stateChanges.next();
    }
    private _disabled = false;

    errorState = false;
    controlType = 'number-input';
    autofilled? = false;

    onChanged = (_: number) => {};
    onTouched = () => {};

    @Input('aria-describedby') userAriaDescribedBy: string;
    setDescribedByIds(ids: string[]): void {
        this.userAriaDescribedBy = ids.join(' ');
    }
    onContainerClick(event: MouseEvent): void {
        if ((event.target as Element).tagName.toLowerCase() != 'input') {
            this.elRef.nativeElement.querySelector('input').focus();
        }
    }

    stateChanges = new Subject<void>();

    constructor(@Optional() @Self() public ngControl: NgControl, private fm: FocusMonitor, private elRef: ElementRef<HTMLElement>) {
        if (this.ngControl != null) {
            this.ngControl.valueAccessor = this;
        }
        fm.monitor(elRef, true).subscribe((origin) => {
            this.focused = !!origin;
            this.stateChanges.next();
        });
    }

    writeValue(obj: any): void {
        const number = Number(obj);
        if (!number) return;
        this.value = number;
    }

    registerOnChange(fn: any): void {
        this.onChanged = fn;
    }
    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

    ngOnDestroy() {
        this.stateChanges.complete();
        this.fm.stopMonitoring(this.elRef);
    }
}
