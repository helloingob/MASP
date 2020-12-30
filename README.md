# MASP (Mintos Account Statement Parser)
This program parses [Mintos](https://www.mintos.com/) account statements and tracks current interests and fees. The output file can be imported by [Portfolio Performance](https://www.portfolio-performance.info/).
It was inspired by https://github.com/ChrisRBe/PP-P2P-Parser. 

*This project is not affiliated with Mintos or Portfolio Performance.*

## Note
This is currently only for the **GERMAN** language settings for Portfolio Performance!

## Usage
1. Build "dotnet build MintosAccountStatementParser.sln"
2. Execute "MASP.exe 123123-account-statement.csv"
3. Locate output in MASP.exe folder (e.g. masp_30122020122237.csv)
4. Import into Portfolio Performance
