/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HomepostComponent } from './homepost.component';

describe('HomepostComponent', () => {
  let component: HomepostComponent;
  let fixture: ComponentFixture<HomepostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomepostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomepostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
