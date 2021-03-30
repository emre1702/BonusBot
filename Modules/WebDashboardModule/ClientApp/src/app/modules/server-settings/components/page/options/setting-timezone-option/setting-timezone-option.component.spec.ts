import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingTimezoneOptionComponent } from './setting-timezone-option.component';

describe('SettingTimezoneOptionComponent', () => {
  let component: SettingTimezoneOptionComponent;
  let fixture: ComponentFixture<SettingTimezoneOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingTimezoneOptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingTimezoneOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
