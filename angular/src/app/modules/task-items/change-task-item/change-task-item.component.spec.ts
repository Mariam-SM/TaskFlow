import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeTaskItemComponent } from './change-task-item.component';

describe('ChangeTaskItemComponent', () => {
  let component: ChangeTaskItemComponent;
  let fixture: ComponentFixture<ChangeTaskItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChangeTaskItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChangeTaskItemComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
