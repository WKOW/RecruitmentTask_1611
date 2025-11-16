1. Run test using: dotnet test
2. Tests runs only locally.
3. Todo:
- CICD
- timestamp validation 
- retry mechanism to avoid fleaky tests
- Zephyr (or other reporting tooling) implementation
- Appsetings files implementation 


Base issues spotted in https://fakerestapi.azurewebsites.net/index.html: 
1. No authorisation
2. Http protocol used instead of https 
3. No correlationId implemented 
4. No dev/uat envs 
5. Main responses seems to be invalid: no 201, no 204, 
6. Entity id are not guids 
7. No field validation for dates.