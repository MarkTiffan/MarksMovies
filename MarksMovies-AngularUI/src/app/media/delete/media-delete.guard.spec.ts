import { TestBed, async, inject } from '@angular/core/testing';

import { MediaDeleteGuard } from './media-delete.guard';

describe('MediaDeleteGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MediaDeleteGuard]
    });
  });

  it('should ...', inject([MediaDeleteGuard], (guard: MediaDeleteGuard) => {
    expect(guard).toBeTruthy();
  }));
});
