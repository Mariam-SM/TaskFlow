import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskItemDetailsComponent } from './task-item-details.component';

describe('TaskItemDetailsComponent', () => {
  let component: TaskItemDetailsComponent;
  let fixture: ComponentFixture<TaskItemDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskItemDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskItemDetailsComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
