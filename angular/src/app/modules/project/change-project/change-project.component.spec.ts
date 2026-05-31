import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeProjectComponent } from './change-project.component';

describe('ChangeProjectComponent', () => {
  let component: ChangeProjectComponent;
  let fixture: ComponentFixture<ChangeProjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChangeProjectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChangeProjectComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
