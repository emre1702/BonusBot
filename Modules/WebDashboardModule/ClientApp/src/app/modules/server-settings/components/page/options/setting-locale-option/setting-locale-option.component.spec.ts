import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingLocaleOptionComponent } from './setting-locale-option.component';

describe('SettingLocaleOptionComponent', () => {
  let component: SettingLocaleOptionComponent;
  let fixture: ComponentFixture<SettingLocaleOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingLocaleOptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingLocaleOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
