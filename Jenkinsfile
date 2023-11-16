pipeline{
    agent any
    
    triggers{
        pollSCM("* * * * *")
    }
    
    stages {
        stage("MyDeliver"){
            steps{
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]){
                    echo $USERNAME
                    bat 'docker login -u $USERNAME -p $PASSWORD'
                    bat "docker push easvdreter/clearService"
                    }
            }
        }
        stage("Build"){
            steps{
                bat "docker compose up --build clearService"
            }
        }

    }
}