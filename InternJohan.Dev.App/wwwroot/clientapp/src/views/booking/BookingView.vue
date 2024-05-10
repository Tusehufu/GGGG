<template>
    <div class="booking">
        <h1 class="text-center">Välkommen till bokning</h1>
        <div class="container">
            <div class="row">
                <div class="col text-center">
                    <iframe src="https://gislaved.actorsmartbook.se/ResourceBookingRequest.aspx" width="900" height="600"></iframe>
                </div>
            </div>
        </div>
</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
    
export default defineComponent({
  name: 'BookingView',
  components: {
  },
});
</script>
