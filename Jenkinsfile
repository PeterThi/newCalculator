pipeline{
    agent any
    
    triggers{
        pollSCM("* * * * *")
    }
    
    stages {
        stage("Build"){
            steps{
                bat "docker compose up --build clearservice"
            }
        }
    }
}