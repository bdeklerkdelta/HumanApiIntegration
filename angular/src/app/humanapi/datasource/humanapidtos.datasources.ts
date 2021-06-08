import {CollectionViewer, DataSource} from '@angular/cdk/collections';
import { BehaviorSubject, Observable } from 'rxjs';
import { HumanApiDataDto, HumanApiDataDtoListResultDto, HumanApiDataServiceProxy } from '../../../shared/service-proxies/service-proxies';

export class HumanApiDataSource implements DataSource<HumanApiDataDto> {
    public loadingData = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingData.asObservable();
    private humanApiData = new BehaviorSubject<HumanApiDataDto[]>([]);

    constructor(private service: HumanApiDataServiceProxy) {}
    connect(collectionViewer: CollectionViewer): Observable<HumanApiDataDto[]> {
           return this.humanApiData.asObservable();
    }
    disconnect(collectionViewer: CollectionViewer): void {
        this.loadingData.complete();
    }

    loadData() {

        this.loadingData.next(true);

        this.service.getAllHumanApiData()
        .subscribe(data => this.humanApiData.next(data));
    }
}
