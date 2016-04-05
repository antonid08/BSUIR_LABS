import random
import argparse


def generateSection(outputFile, lengthOfSection, rangeOfRandom):
    for symbol in xrange(lengthOfSection):
        symbol = chr(random.randint(rangeOfRandom[0],
                     rangeOfRandom[1]))
        outputFile.write(symbol)


def generateLine(args):
    outputFile = args.f
    numberOfSectionsInLine = args.sections
    sectionsSeparator = args.sections_separator
    lengthOfSection = args.section_length
    linesSeparator = args.lines_separator
    isOnlyNumbers = args.only_numbers

    if isOnlyNumbers:
        rangeOfRandom = [48, 57]
    else:
        rangeOfRandom = [97, 122]

    numberOfSection = 1

    generateSection(outputFile, lengthOfSection, rangeOfRandom)
    while numberOfSection < numberOfSectionsInLine:
        outputFile.write(sectionsSeparator)
        generateSection(outputFile, lengthOfSection, rangeOfRandom)
        numberOfSection += 1

    outputFile.write(linesSeparator)


def generate(args):
    numberOfLines = args.lines

    for line in xrange(numberOfLines):
        generateLine(args)


def parse_args():
    parser = argparse.ArgumentParser()

    parser.add_argument('f', type=argparse.FileType('w'), help='Output file')
    parser.add_argument('-s', '--sections', type=int, default='3',
                        help='Number of sections in line. Default: 2')
    parser.add_argument('--section_length', type=int, default='2',
                        help='Length of each section in line')
    parser.add_argument('-l', '--lines', type=int, default=1000,
                        help='Number of lines in the file. Default: 1000')
    parser.add_argument('--sections_separator', default='\t',
                        help='Symbol which separates sections in lines')
    parser.add_argument('--lines_separator', default='\n',
                        help='Symbol which separates lines.')
    # parser.add_argument('-p', action='store_true', help='Print or no')
    parser.add_argument('-n', '--only_numbers', action='store_true',
                        help='Generate file from only numbers')

    return parser.parse_args()


def main():
    args = parse_args()
    generate(args)


if __name__ == '__main__':
    main()
