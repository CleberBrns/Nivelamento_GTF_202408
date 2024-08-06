import { TestBed } from '@angular/core/testing';

import { CalculadorInvestimentosService } from './calculador-investimentos.service';

describe('CalculadorInvestimentosService', () => {
  let service: CalculadorInvestimentosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CalculadorInvestimentosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
