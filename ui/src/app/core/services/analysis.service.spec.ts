import { TestBed } from '@angular/core/testing';
import { describe, it, expect, beforeEach } from 'vitest';
import { Analysis } from './analysis.service';

describe('Analysis', () => {
  let service: Analysis;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Analysis);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
