import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingEmoteOptionComponent } from './setting-emote-option.component';

describe('SettingEmoteOptionComponent', () => {
  let component: SettingEmoteOptionComponent;
  let fixture: ComponentFixture<SettingEmoteOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingEmoteOptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingEmoteOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
