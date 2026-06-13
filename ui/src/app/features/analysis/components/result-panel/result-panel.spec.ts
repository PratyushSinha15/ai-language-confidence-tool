import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultPanel } from './result-panel';

describe('ResultPanel', () => {
  let component: ResultPanel;
  let fixture: ComponentFixture<ResultPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResultPanel],
    }).compileComponents();

    fixture = TestBed.createComponent(ResultPanel);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
