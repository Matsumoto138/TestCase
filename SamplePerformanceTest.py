from locust import HttpUser, task, between

class ClientUser(HttpUser):
    wait_time = between(5, 15)

    @task
    def viewJobs(self):
        self.client.get("/basvuru/acik-pozisyonlar/")

    @task
    def viewJobsDetails(self):
        jobTitle = "3b-kablaj-tasarim-muhendisi"
        self.client.get(f"/jobs/{jobTitle}")

    @task
    def user_registration_and_login(self):
        
        registration_data = {
            "email": "testuser@example.com",
            "password": "testpassword",
            "repeatPassword": "testpassword"
        }
        response = self.client.post("/hesaplar/signup/", json=registration_data)
        assert response.status_code == 200

       
        login_data = {
            "email": "testuser@example.com",
            "password": "testpassword",
            "repeatPassword": "testpassword"
        }
        response = self.client.post("/hesaplar/login/", json=login_data)
        assert response.status_code == 200