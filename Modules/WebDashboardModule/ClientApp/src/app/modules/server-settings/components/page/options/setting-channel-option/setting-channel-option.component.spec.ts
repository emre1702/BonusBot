import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingChannelOptionComponent } from './setting-channel-option.component';

describe('SettingChannelOptionComponent', () => {
  let component: SettingChannelOptionComponent;
  let fixture: ComponentFixture<SettingChannelOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingChannelOptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingChannelOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
