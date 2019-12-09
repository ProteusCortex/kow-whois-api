import { Controller, Get, Query } from '@nestjs/common';
import { SnapshotsService } from '../snapshots/snapshots.service';
import { ParseBooleanPipe } from './parse-boolean.pipe';
import { PiosService } from './pios.service';

@Controller('pios')
export class PiosController {
	constructor(
		private pios: PiosService,
		private snapshots: SnapshotsService
	) { }

	@Get()
	whois(@Query('domain') domain: string, @Query('fast', new ParseBooleanPipe()) fast: boolean = false) {
		// Execute the query and keep the resulting Promise.
		const result = this.pios.query(domain)
			.then(this.snapshots.createSnapshot);

		const response = fast as boolean === true ? Promise.resolve() : result;
		return response;
	}
}
