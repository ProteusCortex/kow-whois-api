import { Column, CreateDateColumn, Entity, OneToMany, PrimaryColumn, PrimaryGeneratedColumn, UpdateDateColumn, VersionColumn } from 'typeorm';
import { Snapshot } from './snapshot.entity';
import { IDomainCreateDto } from '../domains/dtos/domain-create.dto';

@Entity()
export class Domain {
	@PrimaryGeneratedColumn()
	id: number;

	@PrimaryColumn({ unique: true, nullable: false })
	name: string;

	@Column({ unique: true, nullable: true })
	handle: string;

	@Column({ nullable: true })
	protonum: string;

	@Column({ nullable: true })
	creation: Date;

	@Column({ nullable: true })
	expiration: Date;

	@Column({ nullable: true })
	last_update: Date;

	@Column({ nullable: false, default: false })
	registered: boolean;

	@OneToMany(type => Snapshot, snapshot => snapshot.domain)
	snapshots: Snapshot[];

	@CreateDateColumn()
	created_at: Date;

	@UpdateDateColumn()
	updated_at: Date;

	@VersionColumn()
	version: number;

	constructor(domainDto?: IDomainCreateDto) {
		Object.assign(this, domainDto);
	}
}