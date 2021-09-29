import { TestBed } from '@angular/core/testing';

import { MediaDetailsService } from './media-details.service';

describe('MediaDetailsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MediaDetailsService = TestBed.get(MediaDetailsService);
    expect(service).toBeTruthy();
  });
});
