import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterEventDetailsComponent } from './register-event-details.component';

describe('RegisterEventDetailsComponent', () => {
  let component: RegisterEventDetailsComponent;
  let fixture: ComponentFixture<RegisterEventDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegisterEventDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RegisterEventDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
