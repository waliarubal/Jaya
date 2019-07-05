import { SqlJs } from 'sql.js/module';
import { ElectronHelpers } from './electron-helpers';
import { isNullOrUndefined } from 'util';
import { Buffer } from 'buffer';

export class SQLit {
    private _db: SqlJs.Database;
    private _fileName: string;

    get FileName(): string {
        return this._fileName;
    }

    get IsOpen(): boolean {
        return !isNullOrUndefined(this._db);
    }

    private CreateDatabase(driver: SqlJs.SqlJsStatic): void {
        this._db = new driver.Database();
    }

    async Open(fileName: string): Promise<void> {
        this.Close();

        this._fileName = fileName;
        try {
            let driver = await initSqlJs();

            if (ElectronHelpers.FileExists(fileName)) {
                let buffer = await ElectronHelpers.ReadFileBufferAsync(fileName);
                if (buffer)
                    this._db = new driver.Database(buffer);
                else
                    this.CreateDatabase(driver);
            }
            else
                this.CreateDatabase(driver);

        } catch (ex) {
            console.log(ex);
        }
    }

    async Close(): Promise<void> {
        if (!this.IsOpen)
            return;

        try {
            let data = this._db.export();
            let buffer = new Buffer(data);
            await ElectronHelpers.WriteFileAsync(this.FileName, buffer);
            
            this._db.close();
            this._db = undefined;
        } catch (ex) {
            console.log(ex);
        }
    }
}