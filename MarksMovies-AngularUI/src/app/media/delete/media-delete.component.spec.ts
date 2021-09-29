import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaDeleteComponent } from './media-delete.component';

describe('MediaDeleteComponent', () => {
  let component: MediaDeleteComponent;
  let fixture: ComponentFixture<MediaDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MediaDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MediaDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
