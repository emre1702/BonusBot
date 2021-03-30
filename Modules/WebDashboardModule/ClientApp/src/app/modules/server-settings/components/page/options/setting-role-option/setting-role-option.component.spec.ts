import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingRoleOptionComponent } from './setting-role-option.component';

describe('SettingRoleOptionComponent', () => {
  let component: SettingRoleOptionComponent;
  let fixture: ComponentFixture<SettingRoleOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingRoleOptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingRoleOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
