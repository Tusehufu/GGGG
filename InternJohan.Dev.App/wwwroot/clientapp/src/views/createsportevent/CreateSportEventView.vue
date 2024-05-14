<template>
    <div class="create-event">
        <h1 class="text-center">Skapa nytt sportevenemang</h1>
        <form @submit.prevent="submitForm">
            <div class="mb-3">
                <label for="sport" class="form-label">Sport</label>
                <input type="text" id="sport" v-model="newEvent.sport" class="form-control" required />
            </div>
            <div class="mb-3">
                <label for="neededParticipants" class="form-label">Antal deltagare som behövs</label>
                <input type="number" id="neededParticipants" v-model="newEvent.neededParticipants" class="form-control" required />
            </div>
            <div class="mb-3">
                <label for="participants" class="form-label">Antal deltagare just nu</label>
                <input type="number" id="participants" v-model="newEvent.participants" class="form-control" />
            </div>
            <div class="mb-3">
                <label for="dateTime" class="form-label">Datum och tid</label>
                <input type="datetime-local" id="dateTime" v-model="newEvent.dateTime" class="form-control" required />
            </div>
            <div class="mb-3">
                <label for="location" class="form-label">Plats</label>
                <input type="text" id="location" v-model="newEvent.location" class="form-control" required />
            </div>
            <button type="submit" class="btn btn-primary">Skapa evenemang</button>
        </form>
        <!-- Visa felmeddelandet om något går fel -->
        <div v-if="errorMessage" class="alert alert-danger mt-3">
            {{ errorMessage }}
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref } from 'vue';
    import axios from 'axios';

    export default defineComponent({
        name: 'CreateSportEventView',
        setup() {
            // Definiera en ref för att lagra användarens ID
            const userId = localStorage.getItem('userId');

            // Definiera ett objekt för att lagra information om det nya sportevenemanget
            const newEvent = ref({
                sport: '',
                neededParticipants: 0,
                participants: 0,
                dateTime: '',
                location: '',
                userHostId: userId ? parseInt(userId) : null, // Kontrollera om userId är definierad och konvertera det till ett nummer
            });

            // Lägg till en ref för att lagra felmeddelandet
            const errorMessage = ref('');

            // Funktion för att skicka formuläret
            const submitForm = async () => {
                    // Hämta JWT-token från din autentiseringskälla (t.ex. lokal lagring)
                    const token = localStorage.getItem('jwtToken');

                    // Skapa en HTTP-begäran med autentiseringsuppgifter (JWT-token)
                    const response = await axios.post('https://localhost:7056/api/SportEvent', {
                        ...newEvent.value,
                        userHostId: userId ? parseInt(userId) : null,
                    }, {
                        headers: {
                            Authorization: `Bearer ${token}`, // Skicka JWT-token som en del av Authorization-headers
                        },
                    });
            };

            return {
                newEvent,
                submitForm,
            };
        },
    });
</script>

<style>
    /* Lägg till din CSS här om du vill */
</style>
