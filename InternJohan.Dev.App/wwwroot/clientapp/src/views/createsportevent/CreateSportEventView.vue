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
        <!-- Visa meddelandet när evenemanget har skapats -->
        <div v-if="successMessage" class="alert alert-success mt-3">
            {{ successMessage }}
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref } from 'vue';
    import axios from 'axios';

    export default defineComponent({
        name: 'CreateSportEventView',
        setup() {
            // Definiera ett objekt för att lagra information om det nya sportevenemanget
            const newEvent = ref({
                sport: '',
                neededParticipants: 0,
                participants: 0,
                dateTime: '',
                location: '',
            });

            // Skapa en ref för att lagra framgångsmeddelandet
            const successMessage = ref('');

            // Funktion för att skicka formuläret
            const submitForm = async () => {
                try {
                    // Skicka POST-förfrågan till API:et
                    const response = await axios.post('https://localhost:7056/api/SportEvent', newEvent.value);

                    // Visa meddelandet om framgång
                    successMessage.value = 'Evenemanget har skapats framgångsrikt!';

                    // Rensa formuläret
                    newEvent.value = {
                        sport: '',
                        neededParticipants: 0,
                        participants: 0,
                        dateTime: '',
                        location: '',
                    };
                } catch (error) {
                    console.error('Fel vid skapandet av evenemang:', error);
                }
            };

            return {
                newEvent,
                successMessage,
                submitForm,
            };
        },
    });
</script>

<style>
    /* Lägg till din CSS här om du vill */
</style>
