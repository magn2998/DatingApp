/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PostCreationComponent } from './postCreation.component';

describe('PostCreationComponent', () => {
  let component: PostCreationComponent;
  let fixture: ComponentFixture<PostCreationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostCreationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
