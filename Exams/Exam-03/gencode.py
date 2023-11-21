import random

EAN_LENGTH = 13
WEIGHT_EVEN = 1
WEIGHT_ODD = 3
BASE_10 = 10

def isEven(n:int)->bool:
    return n % 2 == 0

def getWeight(index:int)->int:
    if isEven(index):
        return WEIGHT_EVEN
    else:
        return WEIGHT_ODD

def getCheckDigit(baseEanNumber:str)->str:
    baseEanLength = len(baseEanNumber)
    digit=0
    partialSum=0

    for index in range(baseEanLength):
        digit = int(baseEanNumber[index])
        partialSum += digit * getWeight(index)

    checkDigit = BASE_10 - (partialSum % BASE_10)

    if checkDigit == BASE_10:
        checkDigit = 0
    
    return str(checkDigit)

def generateEan13Number(countryCode:str, manufacturerCode:str)->str:
    randomDigits = random.Random()

    baseEanLength = EAN_LENGTH - 1

    sbBaseEanNumber = []

    digit = 0

    for index in range(baseEanLength):
        digit = randomDigits.randint(0,9)
        sbBaseEanNumber.append(str(digit))

    bBaseEanNumber = ''.join(sbBaseEanNumber)

    return f"{bBaseEanNumber}{getCheckDigit(bBaseEanNumber)}"



eanCode = generateEan13Number("57", "12345")
print(eanCode)