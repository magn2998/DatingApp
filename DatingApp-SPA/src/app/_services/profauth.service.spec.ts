/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ProfauthService } from './profauth.service';

describe('Service: Profauth', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProfauthService]
    });
  });

  it('should ...', inject([ProfauthService], (service: ProfauthService) => {
    expect(service).toBeTruthy();
  }));
});
