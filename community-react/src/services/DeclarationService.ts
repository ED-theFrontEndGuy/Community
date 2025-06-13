import { IDeclaration } from "@/types/domain/IDeclaration";
import { EntityService } from "./EntityService";
import { IDeclarationAdd } from "@/types/domain/IDeclarationAdd";


export class DeclarationService extends EntityService<IDeclaration, IDeclarationAdd> {
	constructor() {
		super('declarations');
	}
}
