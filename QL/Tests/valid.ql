form Box1HouseOwning {
    hasSoldHouse: "Did you sell a house in 2010?" boolean
    hasBoughtHouse: "Did you by a house in 2010?" boolean
    hasMaintLoan: "Did you enter a loan for maintenance/reconstruction?" boolean
    if (hasSoldHouse) {
        sellingPrice: "Price the house was sold for:" num
        privateDebt: "Private debts for the sold house:" num
        valueResidue: "Value residue:" num(sellingPrice - privateDebt)
    } else if (hasBoughtHouse)
        buyPrice: "Price the house was bought for:" num
}