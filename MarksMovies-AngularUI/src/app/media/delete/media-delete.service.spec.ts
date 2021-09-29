import { TestBed } from '@angular/core/testing';

import { MediaDeleteService } from './media-delete.service';

describe('MediaDeleteService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MediaDeleteService = TestBed.get(MediaDeleteService);
    expect(service).toBeTruthy();
  });
});
