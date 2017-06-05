import { ToastyService } from 'ng2-toasty';
import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";
import * as Raven from 'raven-js';

export class AppErrorHandler implements ErrorHandler {

    constructor(private ngZone: NgZone, @Inject(ToastyService) private toastyService: ToastyService) {

    }

    handleError(error: any): void {
        if (!isDevMode())
            Raven.captureException(error.originalError || error);
        else
            throw error;

        this.ngZone.run(()=> {
            this.toastyService.error({
                title: 'Error',
                msg: 'An expected error happned.',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
        });
    }

}