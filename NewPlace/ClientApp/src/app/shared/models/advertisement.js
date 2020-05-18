"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Advertisement = /** @class */ (function () {
    function Advertisement() {
        this.id = 0;
        this.title = "";
        this.createDate = "";
        this.validityTime = "";
        this.userName = "";
        this.estateArea = "";
        this.estateAddress = "";
        this.estateCity = "";
        this.price = 0;
        this.provision = 0;
        this._totalCost = 0;
        this.thumbnail = "";
    }
    Object.defineProperty(Advertisement.prototype, "totalCost", {
        get: function () {
            this._totalCost = +this.price + +this.provision; // todo add utilites cost
            return this._totalCost;
        },
        enumerable: true,
        configurable: true
    });
    return Advertisement;
}());
exports.Advertisement = Advertisement;
//# sourceMappingURL=advertisement.js.map