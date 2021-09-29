import { TestBed, async, inject } from '@angular/core/testing';

import { MediaDetailsGuard } from './media-details.guard';

describe('MediaDetailsGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MediaDetailsGuard]
    });
  });

  it('should ...', inject([MediaDetailsGuard], (guard: MediaDetailsGuard) => {
    expect(guard).toBeTruthy();
  }));
});
