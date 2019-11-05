/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ProfregisterComponent } from './profregister.component';

describe('ProfregisterComponent', () => {
  let component: ProfregisterComponent;
  let fixture: ComponentFixture<ProfregisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfregisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfregisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
