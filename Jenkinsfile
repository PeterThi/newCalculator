pipeline{
    agent any
    
    triggers{
        pollSCM("* * * * *")
    }
    
    stages {
        stage("MyDeliver"){
            steps{
                withCredentials([usernamePassword(credentialsId: 'a7253184-e15f-4113-9993-13f5612ca541', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')])
                    bat 'docker login -u $USERNAME -p $PASSWORD'
                    bat "docker push easvdreter/clearService"
            }
        }
        stage("Build"){
            steps{
                bat "docker compose up --build clearService"
            }
        }

    }
}