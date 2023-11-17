pipeline{
    agent any
    
    triggers{
        pollSCM("* * * * *")
    }
    
    stages {

        stage("Build"){
            steps{
                bat "docker compose build"
            }
        }
        stage("MyDeliver"){
            steps{
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]){
                    bat 'docker login -u %USERNAME% -p %PASSWORD%'
                    bat "docker push easvdreter/clearService"
                  }
            }
        }

    }
}