import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { HumanApiDataServiceProxy } from '@shared/service-proxies/service-proxies';
import { HumanApiDataSource } from '../datasource/humanapidtos.datasources';

@Component({
  templateUrl: './display-humanapi.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DisplayHumanApiComponent extends AppComponentBase implements OnInit {
  dataSource: HumanApiDataSource;
  displayedColumns = ['id', 'date', 'duration', 'distance', 'humanId', 'light', 'moderate', 'sedentary', 'source'];
  constructor(injector: Injector,
    private _humanApiService: HumanApiDataServiceProxy) {
    super(injector);
  }
  ngOnInit(): void {
    this.dataSource = new HumanApiDataSource(this._humanApiService);
    this.dataSource.loadData();
  }
}
