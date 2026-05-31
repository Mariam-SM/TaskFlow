import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteTaskItemComponent } from './delete-task-item.component';

describe('DeleteTaskItemComponent', () => {
  let component: DeleteTaskItemComponent;
  let fixture: ComponentFixture<DeleteTaskItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteTaskItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteTaskItemComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
