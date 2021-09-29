import { TestBed } from '@angular/core/testing';

import { MediaCreateService } from './media-create.service';

describe('MediaCreateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MediaCreateService = TestBed.get(MediaCreateService);
    expect(service).toBeTruthy();
  });
});
