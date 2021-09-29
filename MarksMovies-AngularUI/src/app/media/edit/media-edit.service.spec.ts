import { TestBed } from '@angular/core/testing';

import { MediaEditService } from './media-edit.service';

describe('MediaEditService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MediaEditService = TestBed.get(MediaEditService);
    expect(service).toBeTruthy();
  });
});
