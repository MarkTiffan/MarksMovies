import { TestBed, async, inject } from '@angular/core/testing';

import { MediaEditGuard } from './media-edit.guard';

describe('MediaEditGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MediaEditGuard]
    });
  });

  it('should ...', inject([MediaEditGuard], (guard: MediaEditGuard) => {
    expect(guard).toBeTruthy();
  }));
});
